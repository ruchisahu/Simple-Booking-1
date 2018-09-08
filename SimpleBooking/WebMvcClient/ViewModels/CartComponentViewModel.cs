namespace WebMvcClient.ViewModels
{
    public class CartComponentViewModel
    {
        public int ItemsInCart { get; set; }

        public string Disabled => (ItemsInCart == 0) ? "is-disabled" : "";
    }
}
