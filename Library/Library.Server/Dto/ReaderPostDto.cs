﻿namespace Library.Server.Dto;
/// <summary>
/// Class ReaderPostDto is used to store info about readers
/// </summary>
public class ReaderPostDto
{
    /// <summary>
    /// FullName stores reader's fullname
    /// </summary>
    public string FullName { set; get; } = string.Empty;
    /// <summary>
    /// Address stores reader's address
    /// </summary>
    public string Address { set; get; } = string.Empty;
    /// <summary>
    /// Phone stores reader's phone number
    /// </summary>
    public string Phone { set; get; } = string.Empty;
    /// <summary>
    /// RegistrationDate stores reader's registration date
    /// </summary>
    public DateTime RegistrationDate { set; get; }
}