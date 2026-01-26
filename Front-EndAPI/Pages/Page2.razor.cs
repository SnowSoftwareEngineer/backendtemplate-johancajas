
using Front_EndAPI.Pages;

public class Page2Base : ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; } = default!;

    protected Page2? example;

    protected override async Task OnInitializedAsync()
    {
        await LoadExample();
    }

    protected async Task LoadExample()
    {
        example = await Http.GetFromJsonAsync<Example>("Example/items") ?? new();
    }
}