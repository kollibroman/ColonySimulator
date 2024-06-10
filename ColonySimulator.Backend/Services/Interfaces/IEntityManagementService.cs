namespace ColonySimulator.Backend.Services.Interfaces;

/// <summary>
/// Manages entity's quantity
/// </summary>
public interface IEntityManagementService
{
    /// <summary>
    /// Generates new entity
    /// </summary>
    public Task GenerateNewEntity(int summaricCount, CancellationToken ct);
    
    /// <summary>
    /// Cleans up dead entities
    /// </summary>
    /// <returns></returns>
    public Task CleanupDeadEntities(CancellationToken ct);
    
    /// <summary>
    /// Handle health of entities
    /// </summary>
    /// <param name="ct"></param>
    public Task CheckSickStatus(CancellationToken ct);
    
    /// <summary>
    /// Handle Hunger of entities
    /// </summary>
    /// <param name="ct"></param>
    public Task CheckHungerStatus(CancellationToken ct);
}