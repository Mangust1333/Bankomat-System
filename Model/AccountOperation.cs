namespace Model;

public record AccountOperation(long Id, long AccountId, Operation Operation, decimal Balance);