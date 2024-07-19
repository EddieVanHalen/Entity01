using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EntityFrameWorkHomeWork01.DbContext;
using EntityFrameWorkHomeWork01.Interfaces;
using EntityFrameWorkHomeWork01.Model;
using EntityFrameWorkHomeWork01.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameWorkHomeWork01.ViewModel;

[INotifyPropertyChanged]
public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty] private Item? _itemToCreate = new();
    [ObservableProperty] private int _selectedItemIndex = -1;
    [ObservableProperty] private bool _isAddButtonActive = true;
    [ObservableProperty] private bool _isUpdateButtonActive;
    [ObservableProperty] private bool _isDeleteButtonActive;
    public ObservableCollection<Item> Items { get; set; }

    public ApplicationDbContext DbContext { get; set; }

    public MainPageViewModel()
    {
        DbContext = App.Provider.GetService<ApplicationDbContext>()!;

        Items = new ObservableCollection<Item>();

        LoadDbData();
    }

    private async Task LoadDbData()
    {
        Items.Clear();

        foreach (var item in DbContext.Items.ToList())
        {
            Items.Add(item);
        }
    }

    [RelayCommand]
    private void SelectionChanged()
    {
        if (SelectedItemIndex != -1)
        {
            IsDeleteButtonActive = true;
            IsUpdateButtonActive = true;
            ItemToCreate = Items[SelectedItemIndex];
            return;
        }

        IsDeleteButtonActive = false;
        IsUpdateButtonActive = false;
    }

    [RelayCommand]
    private async Task Add()
    {
        if (ItemToCreate!.Year <= 0 || string.IsNullOrWhiteSpace(ItemToCreate!.StNumber) ||
            string.IsNullOrWhiteSpace(ItemToCreate!.Model) ||
            string.IsNullOrWhiteSpace(ItemToCreate!.Make))
        {
            App.Provider.GetService<IMessageBox>()!.Print("Something Went Wrong");
            return;
        }

        ItemToCreate.Id = 0;

        await DbContext.Items.AddAsync(ItemToCreate);

        await DbContext.SaveChangesAsync();

        Items.Add(ItemToCreate);

        ItemToCreate = new();

        App.Provider.GetService<IMessageBox>()!.Print("Succesfully Added!!!");
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedItemIndex == -1)
        {
            return;
        }

        await DbContext.Items.Where(i => i.Id == Items[SelectedItemIndex].Id).ExecuteDeleteAsync();

        Items.RemoveAt(SelectedItemIndex);

        ItemToCreate = new();

        App.Provider.GetService<IMessageBox>()!.Print("Succesfully Deleted!!!");
    }

    [RelayCommand]
    private async Task Update()
    {
        if (ItemToCreate!.Year <= 0 || string.IsNullOrWhiteSpace(ItemToCreate!.StNumber) ||
            string.IsNullOrWhiteSpace(ItemToCreate!.Model) ||
            string.IsNullOrWhiteSpace(ItemToCreate!.Make))
        {
            return;
        }

        await DbContext.Items.Where(i => i.Id == Items[SelectedItemIndex].Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.Model, ItemToCreate!.Model)
                .SetProperty(b => b.Make, ItemToCreate!.Make)
                .SetProperty(b => b.StNumber, ItemToCreate!.StNumber)
                .SetProperty(b => b.Year, ItemToCreate!.Year));

        await LoadDbData();

        SelectedItemIndex = -1;

        ItemToCreate = new();

        App.Provider.GetService<IMessageBox>()!.Print("Succesfully Updated!!!");
    }
}