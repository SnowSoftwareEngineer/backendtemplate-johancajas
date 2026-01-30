
using Front_EndAPI.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

public class Page2Base : ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; } = default!;

    protected List<TestItem> items = new();
    protected bool loading = false;
    protected string? error;

    protected bool showModal = false;
    protected bool isEditing = false;
    protected int editingId = 0;
    protected string modalName = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadItems();
    }

    protected async Task LoadItems()
    {
        loading = true;
        error = null;
        try
        {
            var result = await Http.GetFromJsonAsync<List<TestItem>>("Test/items");
            if (result != null)
            {
                items = result;
            }
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            loading = false;
        }
    }

    protected async Task SaveItem()
    {
        if (string.IsNullOrWhiteSpace(modalName))
        {
            error = "Name is required.";
            return;
        }

        try
        {
            if (isEditing)
            {
                var updated = new TestItem { Id = editingId, Name = modalName };
                var resp = await Http.PutAsJsonAsync($"Test/items/{editingId}", updated);
                if (!resp.IsSuccessStatusCode)
                {
                    error = $"Update failed: {resp.StatusCode}";
                    return;
                }
            }
            else
            {
                var created = new TestItem { Name = modalName };
                var resp = await Http.PostAsJsonAsync("Test/items", created);
                if (!resp.IsSuccessStatusCode)
                {
                    error = $"Create failed: {resp.StatusCode}";
                    return;
                }
            }

            CloseModal();
            await LoadItems();
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
    }

    protected async Task DeleteItem(int id)
    {
        try
        {
            var resp = await Http.DeleteAsync($"Test/items/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                error = $"Delete failed: {resp.StatusCode}";
                return;
            }

            await LoadItems();
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
    }

    protected void OpenCreateModal()
    {
        isEditing = false;
        editingId = 0;
        modalName = string.Empty;
        showModal = true;
        error = null; 
    }

    protected void OpenEditModal(TestItem item)
    {
        isEditing = true;
        editingId = item.Id;
        modalName = item.Name;
        showModal = true;
        error = null;
    }

    protected void CloseModal()
    {
        showModal = false;
        modalName = string.Empty;
        isEditing = false;
        editingId = 0;
        error = null;
    }
}
