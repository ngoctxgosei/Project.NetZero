namespace WS.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetDefaultTemplate(int? tenantId);
    }
}
