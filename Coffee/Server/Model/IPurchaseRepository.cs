namespace Coffee;
public interface IPurchaseRepository
{
    List<Purchase> GetAllPurchases();
    Purchase GetPurchaseById(int id);
    void AddPurchase(Purchase purchase);
    void DeletePurchase(int id);
}