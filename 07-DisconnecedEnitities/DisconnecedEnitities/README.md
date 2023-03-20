https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio

## Saving single entities
If it is known whether or not an insert or update is needed, then either Add or Update can be used appropriately:

```c#
public static void Insert(DbContext context, object entity)
{
    context.Add(entity);
    context.SaveChanges();
}

public static void Update(DbContext context, object entity)
{
    context.Update(entity);
    context.SaveChanges();
}
```

However, if the entity uses auto-generated key values, then the Update method can be used for both cases:
```c#
public static void InsertOrUpdate(DbContext context, object entity)
{
    context.Update(entity);
    context.SaveChanges();
}
```

The Update method normally marks the entity for update, not insert. However, if the entity has an auto-generated key, and no key value has been set, then the entity is instead automatically marked for insert.

If the entity is not using auto-generated keys, then the application must decide whether the entity should be inserted or updated: For example:
```c#
public static void InsertOrUpdate(BloggingContext context, Blog blog)
{
    var existingBlog = context.Blogs.Find(blog.BlogId);
    if (existingBlog == null)
    {
        context.Add(blog);
    }
    else
    {
        context.Entry(existingBlog).CurrentValues.SetValues(blog);
    }

    context.SaveChanges();
}
```
The steps here are:

If Find returns null, then the database doesn't already contain the blog with this ID, so we call Add mark it for insertion.
If Find returns an entity, then it exists in the database and the context is now tracking the existing entity
We then use SetValues to set the values for all properties on this entity to those that came from the client.
The SetValues call will mark the entity to be updated as needed.

> 💡 Tip
> 
> SetValues will only mark as modified the properties that have different values to those in the tracked entity. This means that when the update is sent, only those columns that have actually changed will be updated. (And if nothing has changed, then no update will be sent at all.)

