using PayPalCheckoutSdk.Core;

public static class PayPalClient
{
    public static PayPalHttpClient Client(string clientId, string clientSecret, string environment = "sandbox")
    {
        PayPalEnvironment env;

        if (environment.ToLower() == "live")
            env = new LiveEnvironment(clientId, clientSecret);
        else
            env = new SandboxEnvironment(clientId, clientSecret);

        return new PayPalHttpClient(env);
    }
}
