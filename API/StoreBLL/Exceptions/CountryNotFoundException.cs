namespace StoreBLL.Exceptions;

public class CountryNotFoundException(int countryId) : CountryException($"Country with ID {countryId} was not found.");
