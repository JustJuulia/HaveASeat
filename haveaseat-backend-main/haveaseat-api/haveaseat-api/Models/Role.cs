namespace haveaseat.Models;

/// <summary>
/// This enum represents the possible roles that an user can have.
/// </summary>
/// <remarks>
/// Authorization on the server and client side is not implemented.
/// </remarks>
public enum Role
{
    /// <summary>
    /// The user has administrative privileges.
    /// </summary>
    /// <remarks>
    /// That user can:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// add forbidden dates,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// edit forbidden dates,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// delete forbidden dates,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// book desk,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// add desks,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// delete desk,
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// edit chair position.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>

    ADMIN,

    /// <summary>
    /// The user is an employee.
    /// </summary>
    /// <remarks>
    /// This user can:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// book desks.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    EMPLOYEE
}