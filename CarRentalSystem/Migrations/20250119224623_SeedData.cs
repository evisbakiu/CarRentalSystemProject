using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarClass",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("42fa5c5e-3dfa-482e-bc7b-5ca8adeb42fd"), "SUV" },
                    { new Guid("dcffb19b-e96f-414c-9f13-f685dbe5c12e"), "Van" },
                    { new Guid("eee6fff9-182a-4ae0-8082-e817ad89240b"), "Luxury" },
                    { new Guid("f6b7a80f-9104-419a-b978-5494c2e2d2d4"), "Compact" },
                    { new Guid("fabafed1-9716-4b90-975a-d1aa4cfbfd99"), "Small" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("72ae016b-5002-4b4c-b599-7a6469993f0a"), "Business" },
                    { new Guid("919d30a8-0a60-4be9-bc41-8422070f0a37"), "Economy" },
                    { new Guid("d1e3e1df-b0b3-4d4a-859f-d673207cda56"), "Standard" },
                    { new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"), "Family" },
                    { new Guid("fc85e257-ae4c-4d34-9ebb-b0b829ee8b40"), "Luxury" }
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "CategoryId", "ClassId", "FuelType", "FuelUsage", "Gearbox", "ImagePath", "IsAvailable", "LicensePlate", "Name", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), new Guid("fc85e257-ae4c-4d34-9ebb-b0b829ee8b40"), new Guid("eee6fff9-182a-4ae0-8082-e817ad89240b"), "Petrol", 9m, "Automatic", "/images/mercedes_s_class.jpg", true, "GG-777-HH", "Mercedes-Benz S-Class", 85m, 2023 },
                    { new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"), new Guid("42fa5c5e-3dfa-482e-bc7b-5ca8adeb42fd"), "Hybrid", 4.2m, "Automatic", "/images/toyota_rav4.jpg", true, "EE-555-FF", "Toyota RAV4", 25m, 2022 },
                    { new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), new Guid("72ae016b-5002-4b4c-b599-7a6469993f0a"), new Guid("eee6fff9-182a-4ae0-8082-e817ad89240b"), "Diesel", 8.5m, "Automatic", "/images/bmw_7_series.jpg", true, "HH-888-II", "BMW 7 Series", 80m, 2023 },
                    { new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"), new Guid("42fa5c5e-3dfa-482e-bc7b-5ca8adeb42fd"), "Petrol", 6m, "Automatic", "/images/honda_crv.jpg", true, "FF-666-GG", "Honda CR-V", 27m, 2023 },
                    { new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), new Guid("919d30a8-0a60-4be9-bc41-8422070f0a37"), new Guid("fabafed1-9716-4b90-975a-d1aa4cfbfd99"), "Petrol", 4m, "Manual", "/images/fiat_panda.jpg", true, "AA-111-BB", "Fiat Panda", 12m, 2020 },
                    { new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"), new Guid("dcffb19b-e96f-414c-9f13-f685dbe5c12e"), "Diesel", 8m, "Manual", "/images/vw_transporter.jpg", true, "II-999-JJ", "Volkswagen Transporter", 40m, 2021 },
                    { new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), new Guid("d1e3e1df-b0b3-4d4a-859f-d673207cda56"), new Guid("f6b7a80f-9104-419a-b978-5494c2e2d2d4"), "Diesel", 5.1m, "Automatic", "/images/vw_golf.jpg", true, "CC-333-DD", "Volkswagen Golf", 18m, 2021 },
                    { new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"), new Guid("dcffb19b-e96f-414c-9f13-f685dbe5c12e"), "Diesel", 7.5m, "Manual", "/images/ford_transit.jpg", true, "JJ-101-KK", "Ford Transit", 38m, 2022 },
                    { new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), new Guid("d1e3e1df-b0b3-4d4a-859f-d673207cda56"), new Guid("f6b7a80f-9104-419a-b978-5494c2e2d2d4"), "Petrol", 4.8m, "Manual", "/images/ford_focus.jpg", true, "DD-444-EE", "Ford Focus", 17m, 2020 },
                    { new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), new Guid("919d30a8-0a60-4be9-bc41-8422070f0a37"), new Guid("fabafed1-9716-4b90-975a-d1aa4cfbfd99"), "Petrol", 3.8m, "Manual", "/images/hyundai_i10.jpg", true, "BB-222-CC", "Hyundai i10", 10m, 2019 }
                });

            migrationBuilder.InsertData(
                table: "CarFeature",
                columns: new[] { "Id", "CarId", "Name" },
                values: new object[,]
                {
                    { new Guid("02177695-1885-44b7-9924-2edf325140ac"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), "Electric Windows" },
                    { new Guid("1abb0070-d6f2-4485-81d9-353201ea73a1"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), "Central Locking" },
                    { new Guid("2889a221-08ff-4052-b36a-502b920c1e3f"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), "Central Locking" },
                    { new Guid("2a65f00c-8777-4587-b846-eb8985c7b4d2"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), "Airbags" },
                    { new Guid("2aa8f3f0-4299-4618-a9b9-3e0da526f356"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), "Central Locking" },
                    { new Guid("2b430bc4-55a5-46b9-a877-45e56410dd27"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), "A/C" },
                    { new Guid("34f69403-ee66-4319-8d88-24993a5f8812"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), "A/C" },
                    { new Guid("36813636-dd15-42da-ad00-34130c4e3f96"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), "A/C" },
                    { new Guid("3b791f20-4a08-413a-bc34-27c272d1dd48"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), "A/C" },
                    { new Guid("41271ddc-c732-4049-a707-efd0933ae152"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), "Electric Windows" },
                    { new Guid("421bb528-c236-472d-821d-db6d66e1c5ef"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), "Central Locking" },
                    { new Guid("4389ce0a-1a7a-47ad-854a-c5d1aaaedc95"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), "Electric Windows" },
                    { new Guid("4510164a-bd2d-4f2c-bf37-78fd1347c03d"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), "Central Locking" },
                    { new Guid("46a94eaa-0e5c-4aa4-a4d1-f1327549c092"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), "Electric Windows" },
                    { new Guid("5327954f-df5a-43de-bff9-91ddd5da12eb"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), "Airbags" },
                    { new Guid("5c987387-55c4-4be8-962b-caf50e3f8aac"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), "Central Locking" },
                    { new Guid("6614ccd8-d682-45f0-9161-5cfb4380c7fd"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), "Electric Windows" },
                    { new Guid("685b6fb1-71af-44ef-a8e5-aa80b6443cd4"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), "Airbags" },
                    { new Guid("70e9c956-25e4-4437-ad7c-0cb736cfa723"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), "A/C" },
                    { new Guid("7411bad5-ee8d-432e-9d17-e626426b94cd"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), "Central Locking" },
                    { new Guid("7c3b1325-0b52-4b2f-992f-a1a884016992"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), "Central Locking" },
                    { new Guid("839dd559-0831-45e1-b344-f4e4526f17c2"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), "A/C" },
                    { new Guid("85003d6d-3946-46bf-afed-1f81ab4d5e88"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), "Electric Windows" },
                    { new Guid("89a432c5-f35a-4503-ab77-02290a79c9d6"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), "Electric Windows" },
                    { new Guid("8cbc28d6-912d-44d1-bc7f-14458b5c18b2"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), "Central Locking" },
                    { new Guid("917864bb-8597-4b8d-a1e1-922c48053b4d"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), "Electric Windows" },
                    { new Guid("9fc316fb-1d5c-4804-b7a2-dace50be3ffb"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), "Airbags" },
                    { new Guid("a0729afa-9703-4740-ab6e-6a9226066c98"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), "Airbags" },
                    { new Guid("a07dca6e-6214-426b-8d5a-fdcaf80185d0"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), "Central Locking" },
                    { new Guid("a81ce5ff-886f-46db-80f0-fed14061e091"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), "Electric Windows" },
                    { new Guid("b1aa972f-182f-4537-814d-c112979a6e21"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), "Airbags" },
                    { new Guid("bcf53c18-69cc-494b-964b-79e7cdf83bf1"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), "Airbags" },
                    { new Guid("c196298e-a4e3-46b8-be09-a652347bf0eb"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), "Airbags" },
                    { new Guid("c73dff10-885a-404f-ae5c-08f8f982e469"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), "A/C" },
                    { new Guid("c768da70-33ec-4f96-a614-e1511a16b305"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), "Electric Windows" },
                    { new Guid("dcf05b6e-c5c3-4408-aaab-b537e9b67a0c"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), "Airbags" },
                    { new Guid("df5dfdca-2afc-487e-b18e-bd90aa42a77f"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), "Airbags" },
                    { new Guid("f4c6b332-8c4d-4835-a13d-fe479bfa0f95"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), "A/C" },
                    { new Guid("f6ead864-732a-4637-a13a-207b587b49e3"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), "A/C" },
                    { new Guid("fc4f54ef-1ec0-4721-917b-6286fa0c0a35"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), "A/C" }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("01138ed1-f463-4631-a25e-7eae1fd5bb42"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), 10, 4, 9.0m },
                    { new Guid("05eeb852-5a76-4d82-9fbb-710c6c6ad89f"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), 365, 21, 60.00m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("094ca8b3-ca7e-40f8-81d6-bd23bed0b997"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), 15, 11, 15.30m },
                    { new Guid("176109b7-7c4c-4d71-948c-dfd18a378f12"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), 3, 1, 10m },
                    { new Guid("2165262f-eb91-4471-9002-4db5d1712125"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), 365, 21, 12.75m },
                    { new Guid("25679e55-e11c-4642-ab0e-ab691a3ab1d0"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), 15, 11, 34.00m },
                    { new Guid("28ff1b59-1c92-42c5-a4ab-8787ae1b5dac"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), 20, 16, 30.4m },
                    { new Guid("2d50abbb-483c-40d3-89f7-00c0b16b2bec"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), 15, 11, 21.25m },
                    { new Guid("2f0c6679-f759-48f2-99ff-1da965535eb7"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), 20, 16, 9.6m },
                    { new Guid("3bfec33c-166c-4d54-9203-a6e2fc8c6533"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), 15, 11, 8.50m },
                    { new Guid("4c15bdab-4deb-440f-b7ba-d910db6cdbb8"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), 3, 1, 12m },
                    { new Guid("4d6bb027-e953-48dc-ba22-4bb68b5de0f1"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), 3, 1, 27m },
                    { new Guid("4d86f19c-2746-4dd6-a019-fcd9ecb0c98f"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), 10, 4, 10.8m },
                    { new Guid("4de1edb6-f476-4437-aaaa-44767f45524b"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), 365, 21, 28.50m },
                    { new Guid("4f593c90-fd75-462c-939b-399c733da6c4"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), 20, 16, 20.0m },
                    { new Guid("4f6c5651-4cd2-478d-b6c5-72878211883e"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), 15, 11, 10.20m },
                    { new Guid("511546ed-0201-4c4d-b06c-75d950a28de2"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), 365, 21, 30.00m },
                    { new Guid("53040332-3dda-48ef-836d-8ee64de3d722"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), 365, 21, 20.25m },
                    { new Guid("5e517f32-b316-42eb-9d6d-51c9c85cb701"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), 15, 11, 72.25m },
                    { new Guid("610a5421-c436-4e12-8de3-096e718afe29"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), 365, 21, 7.50m },
                    { new Guid("66553731-66ec-4fa1-b733-824c675217ac"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), 10, 4, 22.5m },
                    { new Guid("66c1cc17-2949-42e9-bbd2-73a9c2eaf099"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), 3, 1, 40m },
                    { new Guid("758251ca-7f6e-4e60-bc48-e04af4bf2e1a"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), 3, 1, 25m },
                    { new Guid("79e9876d-d87f-49f1-8793-067cbfdf785e"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), 10, 4, 16.2m },
                    { new Guid("81fb4ad6-d1bc-48aa-ad5c-ca5f483250ca"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), 15, 11, 68.00m },
                    { new Guid("845dc46a-d0ed-41c5-a174-7ac837b7fb85"), new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"), 365, 21, 9.00m },
                    { new Guid("97f09e3a-35bb-4a84-bcb1-9b00c98eb6b7"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), 15, 11, 32.30m },
                    { new Guid("9df918e1-5234-4303-a24a-dd7d43cde1de"), new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"), 20, 16, 8.0m },
                    { new Guid("a47a828d-f2f3-47c8-8e0e-8b304386b361"), new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"), 365, 21, 18.75m },
                    { new Guid("ab969447-3f91-4827-9148-c8e5ffbc53c6"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), 10, 4, 24.3m },
                    { new Guid("b486b73d-06b0-47fc-bdc5-f3f2a930bdf6"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), 20, 16, 32.0m },
                    { new Guid("b57f6a57-7154-4df1-b1da-71e50f410f87"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), 20, 16, 14.4m },
                    { new Guid("bb32ac30-c816-4715-b693-3eb7e8ce232d"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), 10, 4, 34.2m },
                    { new Guid("bc90c786-4e2b-4231-a9c6-fb36f2ea1251"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), 20, 16, 68.0m },
                    { new Guid("c0164532-4bb4-40c2-a7d6-8c5c4949f66c"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), 3, 1, 18m },
                    { new Guid("c8538439-19c8-483f-8dd8-c442e04c46a8"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), 10, 4, 76.5m },
                    { new Guid("c9c56ef5-29cf-4ef1-82b3-924daa73c502"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), 3, 1, 17m },
                    { new Guid("cd2b781c-23b2-4bed-b1e2-bbbd5eaa3e92"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), 3, 1, 85m },
                    { new Guid("d272660b-b84e-4883-a7ad-663688c8568a"), new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"), 365, 21, 63.75m },
                    { new Guid("decbc42d-7518-482b-874e-7c4724a3e654"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), 15, 11, 14.45m },
                    { new Guid("e0058cee-4746-4f01-893e-b004f43ec0b8"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), 10, 4, 15.3m },
                    { new Guid("e07e6987-fb71-445f-8932-4ff998a5043e"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), 3, 1, 80m },
                    { new Guid("e7d0f5cb-974e-4e7a-8acc-4bb3cfddc3e9"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), 15, 11, 22.95m },
                    { new Guid("ead9d517-ebb3-420d-a72b-a27c8cfb7c45"), new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"), 20, 16, 13.6m }
                });

            migrationBuilder.InsertData(
                table: "PricingTier",
                columns: new[] { "Id", "CarId", "MaxDays", "MinDays", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("ecee0a2e-fecf-46a6-be2d-242ca501ddad"), new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"), 365, 21, 13.50m },
                    { new Guid("ef3ae690-d5f9-4966-8a1e-138a9716febd"), new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"), 20, 16, 21.6m },
                    { new Guid("ef6b1d20-3078-4306-9b88-8030031d91fb"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), 10, 4, 72.0m },
                    { new Guid("f1cdcdc4-b0f2-40cc-9d8e-72ee11041931"), new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"), 20, 16, 64.0m },
                    { new Guid("fa268a36-7915-4620-a940-2ea7e188d6db"), new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"), 3, 1, 38m },
                    { new Guid("ff679268-9618-4b55-ac19-632bf9c941d5"), new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"), 10, 4, 36.0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("02177695-1885-44b7-9924-2edf325140ac"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("1abb0070-d6f2-4485-81d9-353201ea73a1"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("2889a221-08ff-4052-b36a-502b920c1e3f"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("2a65f00c-8777-4587-b846-eb8985c7b4d2"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("2aa8f3f0-4299-4618-a9b9-3e0da526f356"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("2b430bc4-55a5-46b9-a877-45e56410dd27"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("34f69403-ee66-4319-8d88-24993a5f8812"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("36813636-dd15-42da-ad00-34130c4e3f96"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("3b791f20-4a08-413a-bc34-27c272d1dd48"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("41271ddc-c732-4049-a707-efd0933ae152"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("421bb528-c236-472d-821d-db6d66e1c5ef"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("4389ce0a-1a7a-47ad-854a-c5d1aaaedc95"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("4510164a-bd2d-4f2c-bf37-78fd1347c03d"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("46a94eaa-0e5c-4aa4-a4d1-f1327549c092"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("5327954f-df5a-43de-bff9-91ddd5da12eb"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("5c987387-55c4-4be8-962b-caf50e3f8aac"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("6614ccd8-d682-45f0-9161-5cfb4380c7fd"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("685b6fb1-71af-44ef-a8e5-aa80b6443cd4"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("70e9c956-25e4-4437-ad7c-0cb736cfa723"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("7411bad5-ee8d-432e-9d17-e626426b94cd"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("7c3b1325-0b52-4b2f-992f-a1a884016992"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("839dd559-0831-45e1-b344-f4e4526f17c2"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("85003d6d-3946-46bf-afed-1f81ab4d5e88"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("89a432c5-f35a-4503-ab77-02290a79c9d6"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("8cbc28d6-912d-44d1-bc7f-14458b5c18b2"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("917864bb-8597-4b8d-a1e1-922c48053b4d"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("9fc316fb-1d5c-4804-b7a2-dace50be3ffb"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("a0729afa-9703-4740-ab6e-6a9226066c98"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("a07dca6e-6214-426b-8d5a-fdcaf80185d0"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("a81ce5ff-886f-46db-80f0-fed14061e091"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("b1aa972f-182f-4537-814d-c112979a6e21"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("bcf53c18-69cc-494b-964b-79e7cdf83bf1"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("c196298e-a4e3-46b8-be09-a652347bf0eb"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("c73dff10-885a-404f-ae5c-08f8f982e469"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("c768da70-33ec-4f96-a614-e1511a16b305"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("dcf05b6e-c5c3-4408-aaab-b537e9b67a0c"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("df5dfdca-2afc-487e-b18e-bd90aa42a77f"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("f4c6b332-8c4d-4835-a13d-fe479bfa0f95"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("f6ead864-732a-4637-a13a-207b587b49e3"));

            migrationBuilder.DeleteData(
                table: "CarFeature",
                keyColumn: "Id",
                keyValue: new Guid("fc4f54ef-1ec0-4721-917b-6286fa0c0a35"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("01138ed1-f463-4631-a25e-7eae1fd5bb42"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("05eeb852-5a76-4d82-9fbb-710c6c6ad89f"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("094ca8b3-ca7e-40f8-81d6-bd23bed0b997"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("176109b7-7c4c-4d71-948c-dfd18a378f12"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("2165262f-eb91-4471-9002-4db5d1712125"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("25679e55-e11c-4642-ab0e-ab691a3ab1d0"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("28ff1b59-1c92-42c5-a4ab-8787ae1b5dac"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("2d50abbb-483c-40d3-89f7-00c0b16b2bec"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("2f0c6679-f759-48f2-99ff-1da965535eb7"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("3bfec33c-166c-4d54-9203-a6e2fc8c6533"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4c15bdab-4deb-440f-b7ba-d910db6cdbb8"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4d6bb027-e953-48dc-ba22-4bb68b5de0f1"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4d86f19c-2746-4dd6-a019-fcd9ecb0c98f"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4de1edb6-f476-4437-aaaa-44767f45524b"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4f593c90-fd75-462c-939b-399c733da6c4"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("4f6c5651-4cd2-478d-b6c5-72878211883e"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("511546ed-0201-4c4d-b06c-75d950a28de2"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("53040332-3dda-48ef-836d-8ee64de3d722"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("5e517f32-b316-42eb-9d6d-51c9c85cb701"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("610a5421-c436-4e12-8de3-096e718afe29"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("66553731-66ec-4fa1-b733-824c675217ac"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("66c1cc17-2949-42e9-bbd2-73a9c2eaf099"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("758251ca-7f6e-4e60-bc48-e04af4bf2e1a"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("79e9876d-d87f-49f1-8793-067cbfdf785e"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("81fb4ad6-d1bc-48aa-ad5c-ca5f483250ca"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("845dc46a-d0ed-41c5-a174-7ac837b7fb85"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("97f09e3a-35bb-4a84-bcb1-9b00c98eb6b7"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("9df918e1-5234-4303-a24a-dd7d43cde1de"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("a47a828d-f2f3-47c8-8e0e-8b304386b361"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ab969447-3f91-4827-9148-c8e5ffbc53c6"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("b486b73d-06b0-47fc-bdc5-f3f2a930bdf6"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("b57f6a57-7154-4df1-b1da-71e50f410f87"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("bb32ac30-c816-4715-b693-3eb7e8ce232d"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("bc90c786-4e2b-4231-a9c6-fb36f2ea1251"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("c0164532-4bb4-40c2-a7d6-8c5c4949f66c"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("c8538439-19c8-483f-8dd8-c442e04c46a8"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("c9c56ef5-29cf-4ef1-82b3-924daa73c502"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("cd2b781c-23b2-4bed-b1e2-bbbd5eaa3e92"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("d272660b-b84e-4883-a7ad-663688c8568a"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("decbc42d-7518-482b-874e-7c4724a3e654"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("e0058cee-4746-4f01-893e-b004f43ec0b8"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("e07e6987-fb71-445f-8932-4ff998a5043e"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("e7d0f5cb-974e-4e7a-8acc-4bb3cfddc3e9"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ead9d517-ebb3-420d-a72b-a27c8cfb7c45"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ecee0a2e-fecf-46a6-be2d-242ca501ddad"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ef3ae690-d5f9-4966-8a1e-138a9716febd"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ef6b1d20-3078-4306-9b88-8030031d91fb"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("f1cdcdc4-b0f2-40cc-9d8e-72ee11041931"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("fa268a36-7915-4620-a940-2ea7e188d6db"));

            migrationBuilder.DeleteData(
                table: "PricingTier",
                keyColumn: "Id",
                keyValue: new Guid("ff679268-9618-4b55-ac19-632bf9c941d5"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("14e8371b-e721-4407-bfc5-92f83202dd15"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("27bddea7-60ad-4c0f-9c9a-73e91cf19420"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("786e2218-4650-4af8-bbfa-e9d6481c7788"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("9504af07-bf29-4774-a410-49e606c5dc7c"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("955172b4-fa59-4a11-8367-29fa3ca2a566"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("a8a74f6e-ba18-4ace-8e1e-d19087e9a72f"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("a8cc813d-ad3a-41e5-82b1-05ea96be6661"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("c9724455-7c47-4a34-bcf8-3dda661b9af3"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("dbf27af0-42da-44fe-92b4-180e5de3117a"));

            migrationBuilder.DeleteData(
                table: "Car",
                keyColumn: "Id",
                keyValue: new Guid("edf0d66f-745e-473a-9b92-b64c960c1c04"));

            migrationBuilder.DeleteData(
                table: "CarClass",
                keyColumn: "Id",
                keyValue: new Guid("42fa5c5e-3dfa-482e-bc7b-5ca8adeb42fd"));

            migrationBuilder.DeleteData(
                table: "CarClass",
                keyColumn: "Id",
                keyValue: new Guid("dcffb19b-e96f-414c-9f13-f685dbe5c12e"));

            migrationBuilder.DeleteData(
                table: "CarClass",
                keyColumn: "Id",
                keyValue: new Guid("eee6fff9-182a-4ae0-8082-e817ad89240b"));

            migrationBuilder.DeleteData(
                table: "CarClass",
                keyColumn: "Id",
                keyValue: new Guid("f6b7a80f-9104-419a-b978-5494c2e2d2d4"));

            migrationBuilder.DeleteData(
                table: "CarClass",
                keyColumn: "Id",
                keyValue: new Guid("fabafed1-9716-4b90-975a-d1aa4cfbfd99"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("72ae016b-5002-4b4c-b599-7a6469993f0a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("919d30a8-0a60-4be9-bc41-8422070f0a37"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("d1e3e1df-b0b3-4d4a-859f-d673207cda56"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e626bca7-a3af-4316-8af5-75eddb8a96f6"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("fc85e257-ae4c-4d34-9ebb-b0b829ee8b40"));
        }
    }
}
