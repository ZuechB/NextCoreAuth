namespace Models.Mail
{
    public class EmailSystemTemplates
    {
        public static string GetTemplate(Template template)
        {
            switch (template)
            {
                case Template.WelcomeEmail:
                    return "your templateId";
                default:
                    return null;
            }
        }
    }

    public enum Template
    {
        WelcomeEmail // list of all your templates
    }
}
