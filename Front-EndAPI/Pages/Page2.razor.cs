
using Front_EndAPI.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

public class Page2Base : ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; } = default!;

    protected ClassLibrary.Models.Example? example;

    protected override async Task OnInitializedAsync()
    {
        await LoadExample();
    }

    protected async Task LoadExample()
    {
        example = await Http.GetFromJsonAsync<ClassLibrary.Models.Example>("Example/items") ?? new ClassLibrary.Models.Example();
    }
}
