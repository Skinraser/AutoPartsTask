// See https://aka.ms/new-console-template for more information

using ConsoleApp3.Interfaces;
using ConsoleApp3.Repositories;

IBrandRepository create = new BrandRepository();
    await create.Create("https://www.ilcats.ru");

