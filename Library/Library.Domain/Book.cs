﻿namespace Library.Domain;

/// <summary>
/// Class Book is used to store info about the books
/// </summary>
public class Book
{
    /// <summary>
    /// Id stores book's id
    /// </summary>
    public int Id { set; get; }
    /// <summary>
    /// Cipher stores cipher of the book
    /// </summary>
    public string Cipher { set; get; } = string.Empty;
    /// <summary>
    /// Author stores last name and initials of the author
    /// </summary>
    public string Author { set; get; } = string.Empty;
    /// <summary>
    /// Name stores name of the book
    /// </summary>
    public string Name { set; get; } = string.Empty;
    /// <summary>
    /// PlaceEdition stores place where book was published
    /// </summary>
    public string PlaceEdition { set; get; } = string.Empty;
    /// <summary>
    /// YearEdition stores year of book's publication
    /// </summary>
    public int YearEdition { set; get; }
    /// <summary>
    /// TypeEditionId stores id of type book
    /// </summary>
    public int TypeEditionId { set; get; }
    /// <summary>
    /// TypeEdition stores list of types book
    /// </summary>
    public List<TypeEdition> TypeEdition { set; get; } = new List<TypeEdition>();
    /// <summary>
    /// IsIssued stores information about whether a book has been issued
    /// </summary>
    public bool IsIssued { set; get; }
}
