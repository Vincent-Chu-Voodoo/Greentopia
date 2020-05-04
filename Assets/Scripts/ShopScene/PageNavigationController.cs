using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PageEnum
{
    PlantsPage, IngredientsPage, InventoryPage
}

public class PageNavigationController : MonoBehaviour
{
    public List<GameObject> pageList;

    public void NavigateToPlantsPage()
    {
        NavigateToPage(PageEnum.PlantsPage);
    }

    public void NavigateToIngredientsPage()
    {
        NavigateToPage(PageEnum.IngredientsPage);
    }

    public void NavigateToInventoryPage()
    {
        NavigateToPage(PageEnum.InventoryPage);
    }

    public void NavigateToPage(PageEnum pageEnum)
    {
        CloseAllPages();
        pageList[(int) pageEnum].SetActive(true);
    }

    public void CloseAllPages()
    {
        foreach (var page in pageList)
            page.SetActive(false);
    }
}
