using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppEFCore.Data;

// Contexto de la base de datos de contactos.
public class ContactContext : DbContext
{
    // Magic string.
    public static readonly string RowVersion = nameof(RowVersion);

    // Magic strings.
    public static readonly string ContactsDb = nameof(ContactsDb).ToLower();

    // Inject options.
    // options: The DbContextOptions{ContactContext} for the context.
    public ContactContext(DbContextOptions<ContactContext> options)
        : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }

    // List of Contact.
    public DbSet<Contact>? Contacts { get; set; }

    // Definir el modelo.
    // modelBuilder: El ModelBuilder.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Esta propiedad no está en la clase C#,
        // entonces lo configuramos como una propiedad "sombra" y la usamos para la concurrencia.
        modelBuilder.Entity<Contact>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();

        base.OnModelCreating(modelBuilder);
    }

    // Deseche el patrón.
    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        // Libera los recursos asignados para este contexto.
        base.Dispose();
    }

    // Deseche el patrón.
    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        // Libera los recursos asignados para este contexto.
        return base.DisposeAsync();
    }
}
