namespace BankApplication.Extensions;

public static class AccountExtensions
{
    public static List<Account> ToAccount(this IEnumerable<RegistrationViewModel> items) =>
        items.Select(x => x.ToAccount()).ToList();
    public static Account ToAccount(this RegistrationViewModel item) => new()
    {
        PhoneNumber = item.PhoneNumber,
        Email = item.Email,
        Password = item.Password,
        AccountName = item.AccountName,
        AccountSurname = item.AccountSurname
    };
}