
//LINK for password hashing on server: http://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
public static class DataManager 
{
	//public static IDataManager Data = PhpDataManager.instance;
	public static IDataManager Data = AspNetDataManager.instance;
}