console.log("adminDashboard.js loaded 🚀");

document.addEventListener('DOMContentLoaded', function () {
    console.log("DOMContentLoaded fired");

    const filterBtn = document.getElementById('filterBtn');
    if (!filterBtn) {
        console.warn("filterBtn not found!");
        return;
    }

    setDefaultDateRange();
    loadDashboardData();
    filterBtn.addEventListener('click', loadDashboardData);
});

function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(amount);
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' });
}

function toISODateString(input) {
    if (!input) return "";
    const date = new Date(input);
    if (isNaN(date)) return "";
    return date.toISOString().split('T')[0];
}

function setDefaultDateRange() {
    const today = new Date();
    const firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
    const lastDay = new Date(today.getFullYear(), today.getMonth() + 1, 0);
    document.getElementById('startDate').valueAsDate = firstDay;
    document.getElementById('endDate').valueAsDate = lastDay;
}

let reservationsChart = null;
let fuelTypeChart = null;

function loadDashboardData() {
    let startRaw = document.getElementById('startDate').value;
    let endRaw = document.getElementById('endDate').value;

    let startDate = toISODateString(startRaw);
    let endDate = toISODateString(endRaw);

    if (!startDate || !endDate) {
        console.warn("Invalid or missing date inputs:", { startRaw, endRaw });
        return;
    }

    if (new Date(startDate) > new Date(endDate)) {
        [startDate, endDate] = [endDate, startDate]; // swap
    }

    console.log("Formatted & validated dates:", { startDate, endDate });

    // 🔹 Load reservation details
    fetch(`/Admin/GetReservationDetails?startDate=${startDate}&endDate=${endDate}`)
        .then(r => r.json())
        .then(result => {
            console.log("ReservationDetails:", result);
            const data = Array.isArray(result.data?.$values) ? result.data.$values : [];

            const tbody = document.querySelector('#reservationsTable tbody');
            tbody.innerHTML = '';

            if (data.length === 0) {
                tbody.innerHTML = `<tr><td colspan="10" class="text-center text-muted">No reservations found.</td></tr>`;
                return;
            }

            data.forEach(r => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${r.id}</td>
                    <td>${r.carName?.$value || r.carName || '-'}</td>
                    <td>${r.carLicensePlate?.$value || r.carLicensePlate || '-'}</td>
                    <td>${r.userName?.$value || r.userName || '-'}</td>
                    <td>${r.userEmail?.$value || r.userEmail || '-'}</td>
                    <td>${formatDate(r.startDate)}</td>
                    <td>${formatDate(r.endDate)}</td>
                    <td>${r.durationDays} days</td>
                    <td>${formatCurrency(r.totalCost)}</td>
                    <td>
                        <span class="badge ${r.status?.$value === 'Completed' ? 'bg-success' :
                        r.status?.$value === 'Cancelled' ? 'bg-danger' : 'bg-warning'}">
                            ${r.status?.$value || r.status || '-'}
                        </span>
                    </td>
                `;
                tbody.appendChild(row);
            });
        });

    // 🔹 Load car availability
    fetch('/Admin/GetCarAvailabilityRate')
        .then(r => r.json())
        .then(data => {
            console.log("CarAvailabilityRate:", data);
            document.getElementById('availableCars').textContent = data.availableCars;
            document.getElementById('totalCars').textContent = data.totalCars;
            document.getElementById('availabilityRate').textContent = data.availabilityRate;
            document.getElementById('availabilityProgress').style.width = `${data.availabilityRate}%`;
        });

    // 🔹 Load reservations by month
    fetch('/Admin/GetReservationsByMonth')
        .then(r => r.json())
        .then(response => {
            console.log("ReservationsByMonth:", response);
            const data = Array.isArray(response.$values) ? response.$values : [];

            const ctx = document.getElementById('reservationsChart').getContext('2d');
            if (reservationsChart) reservationsChart.destroy();

            reservationsChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: data.map(d => d.month),
                    datasets: [{
                        label: 'Numri i Rezervimeve',
                        data: data.map(d => d.reservationCount),
                        backgroundColor: 'rgba(54, 162, 235, 0.7)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true,
                            title: { display: true, text: 'Numri i Rezervimeve' }
                        }
                    }
                }
            });
        });

    // 🔹 Load fuel type distribution
    fetch('/Admin/GetFuelTypeDistribution')
        .then(r => r.json())
        .then(response => {
            console.log("FuelTypeDistribution:", response);
            const data = Array.isArray(response.$values) ? response.$values : [];

            const ctx = document.getElementById('fuelTypeChart').getContext('2d');
            if (fuelTypeChart) fuelTypeChart.destroy();

            const backgroundColors = [
                'rgba(255, 99, 132, 0.7)', 'rgba(54, 162, 235, 0.7)', 'rgba(255, 206, 86, 0.7)',
                'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)', 'rgba(255, 159, 64, 0.7)'
            ];

            fuelTypeChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: data.map(d => d.fuelType),
                    datasets: [{
                        data: data.map(d => d.count),
                        backgroundColor: backgroundColors.slice(0, data.length),
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'right' },
                        tooltip: {
                            callbacks: {
                                label: function (ctx) {
                                    const value = ctx.raw;
                                    const total = ctx.dataset.data.reduce((acc, val) => acc + val, 0);
                                    const percent = Math.round((value / total) * 100);
                                    return `${ctx.label}: ${value} (${percent}%)`;
                                }
                            }
                        }
                    }
                }
            });
        });
}
