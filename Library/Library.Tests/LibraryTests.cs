namespace Library.Tests;

using System.Linq;

public class LibraryTests : IClassFixture<LibraryFixture>
{
    private readonly LibraryFixture _fixture;

    public LibraryTests(LibraryFixture fixture)
    {
        _fixture = fixture;
    }
    /// <summary>
    /// First request - give info about book by the cipher
    /// </summary>
    [Fact]
    public void CipherTest()
    {
        var fixtureBook = _fixture.FixtureBook.ToList();
        var request = (from book in fixtureBook
                       where book.Cipher == "5698/197b"
                       select book).Count();
        Assert.Equal(1, request);
    }
    /// <summary>
    /// Second request - give info about all books issued order by book's name
    /// </summary>
    [Fact]
    public void BooksTest()
    {
        var fixtureBook = _fixture.FixtureBook.ToList();
        var fixtureCard = _fixture.FixtureCard.ToList();
        var request = (from book in fixtureBook
                       join card in fixtureCard on book.Id equals card.BookId
                       orderby book.Name
                       group book by book.Id into b
                       select b).Count();
        Assert.Equal(4, request);
    }
    /// <summary>
    /// Third request - give info on the availability of the selected book in different departments and their quantity
    /// </summary>
    [Fact]
    public void CountBooksTest()
    {
        var fixtureBook = _fixture.FixtureBook.ToList();
        var fixtureDepartment = _fixture.FixtureDepartment.ToList();
        var request = (from department in fixtureDepartment
                       join book in fixtureBook on department.BookId equals book.Id
                       where book.Id == 3
                       select new { departments = department, count = department.Count }).ToList();
        Assert.Equal(2, request.Count());
        Assert.Equal(15, request.First(x => x.departments.Id == 3).count);
        Assert.Equal(20, request.First(x => x.departments.Id == 4).count);
    }
    /// <summary>
    /// Fourth request - give info about count of books in each department for each type edition
    /// </summary>
    [Fact]
    public void CountTypesTest()
    {
        var fixtureBook = _fixture.FixtureBook.ToList();
        var fixtureTypeEdition = _fixture.FixtureTypeEdition.ToList();
        var fixtureDepartment = _fixture.FixtureDepartment.ToList();
        var request = (from mass in
                       (from department in fixtureDepartment
                        join book in fixtureBook on department.BookId equals book.Id
                        join type in fixtureTypeEdition on book.TypeEditionId equals type.Id
                        select new
                        {
                            types = type.Name,
                            count = department.Count
                        })
                       group mass by mass.types into gr
                       select new
                       {
                           Count = gr.Sum(ret => ret.count),
                           gr.Key
                       }).ToList();
        Assert.Equal(33, request.First(x => x.Key == "Tutorial").Count);
        Assert.Equal(35, request.First(x => x.Key == "Monograph").Count);
        Assert.Equal(75, request.First(x => x.Key == "Methodological guidelines").Count);
    }
    /// <summary>
    /// Fifth request - give info about top 5 readers who have read the most books in a given period
    /// </summary>
    [Fact]
    public void TopFiveTest()
    {
        var fixtureCard = _fixture.FixtureCard.ToList();
        var fixtureReader = _fixture.FixtureReader.ToList();
        var date = new DateTime(2023, 3, 1);
        var numOfReaders = (from card in fixtureCard
                            join reader in fixtureReader on card.ReaderId equals reader.Id
                            where card.DateOfReturn < date
                            group card by reader.Id into g
                            select new
                            {
                                readers = g.Key,
                                count = g.Count()
                            }).ToList();
        var request = (from reader in numOfReaders
                       orderby reader.count descending
                       select reader).Take(5).ToList();
        var first = request.First();
        Assert.Equal(1, first.readers);
        Assert.Equal(5, request.Count);
    }
    /// <summary>
    /// Sixth request - give info about readers who have delayed books for the longest period of time, ordered by full name
    /// </summary>
    [Fact]
    public void DelayReadersTest()
    {
        var fixtureCard = _fixture.FixtureCard.ToList();
        var fixtureReader = _fixture.FixtureReader.ToList();
        var maxDelay = (from card in fixtureCard
                        join reader in fixtureReader on card.ReaderId equals reader.Id
                        group card by reader.FullName into g
                        select new
                        {
                            Delay = g.Key,
                            MaxDay = g.Select(x => (x.DateOfReturn - x.DateOfIssue).TotalDays - x.DayCount).Max(),
                            Count = g.Count()
                        }).ToList();
        var request = (from readers in maxDelay
                       where (readers.MaxDay == maxDelay.Max(x => x.MaxDay))
                       orderby readers.Delay
                       select readers.Count).Count();
        Assert.Equal(2, request);
    }
}