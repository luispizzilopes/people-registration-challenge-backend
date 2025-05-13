namespace PeopleAPI.Domain.Exception.Messages; 

public class PersonMessagesException
{
    public static string CpfInvalid = "O CPF informado é inválido!";
    public static string CpfIsRequired = "A propriedade 'CPF' é necessária para o cadastro da pessoa!"; 
    public static string NameIsRequired = "A propriedade 'Nome' é necessária para o cadastro da pessoa!";
    public static string BirthDateIsRequired = "A propriedade 'Data de Nascimento' é necessária para o cadastro da pessoa!";
    public static string BirthDateInvalid = "A data de nascimento é inválida!"; 
}
