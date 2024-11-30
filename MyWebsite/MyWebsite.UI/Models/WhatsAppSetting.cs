using System.Text.Json.Serialization;

namespace MyWebsite.UI.Models
{
    public class WhatsAppButtonSetting
    {
        [JsonPropertyName("btnColor")]
        public string BtnColor { get; set; }

        [JsonPropertyName("ctaText")]
        public string CtaText { get; set; }

        [JsonPropertyName("cornerRadius")]
        public int CornerRadius { get; set; }

        [JsonPropertyName("marginBottom")]
        public int MarginBottom { get; set; }

        [JsonPropertyName("marginLeft")]
        public int MarginLeft { get; set; }

        [JsonPropertyName("marginRight")]
        public int MarginRight { get; set; }

        [JsonPropertyName("btnPosition")]
        public string BtnPosition { get; set; }

        [JsonPropertyName("whatsAppNumber")]
        public string WhatsAppNumber { get; set; }

        [JsonPropertyName("welcomeMessage")]
        public string WelcomeMessage { get; set; }

        [JsonPropertyName("zIndex")]
        public int ZIndex { get; set; }

        [JsonPropertyName("btnColorScheme")]
        public string BtnColorScheme { get; set; }

    }

    public class DarkHeaderColorScheme
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("subTitle")]
        public string SubTitle { get; set; }
    }

    public class WhatsAppWidgetSetting
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("subTitle")]
        public string SubTitle { get; set; }

        [JsonPropertyName("headerBackgroundColor")]
        public string HeaderBackgroundColor { get; set; }

        [JsonPropertyName("headerColorScheme")]
        public string HeaderColorScheme { get; set; }

        [JsonPropertyName("greetingText")]
        public string GreetingText { get; set; }

        [JsonPropertyName("ctaText")]
        public string CtaText { get; set; }

        [JsonPropertyName("btnColor")]
        public string BtnColor { get; set; }

        [JsonPropertyName("cornerRadius")]
        public int CornerRadius { get; set; }

        [JsonPropertyName("welcomeMessage")]
        public string WelcomeMessage { get; set; }

        [JsonPropertyName("btnColorScheme")]
        public string BtnColorScheme { get; set; }

        [JsonPropertyName("brandImage")]
        public string BrandImage { get; set; }

        [JsonPropertyName("darkHeaderColorScheme")]
        public DarkHeaderColorScheme DarkHeaderColorScheme { get; set; }
    }
}