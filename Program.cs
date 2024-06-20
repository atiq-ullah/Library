using Library;

static async Task DisplayActions()
{
    string choice = TUI.DbActionChoice();
    await ProcessChoice(choice);
}

static async Task ProcessChoice(string choice)
{
    switch (choice)
    {
        case "Add":
            await Handler.Add();
            break;
        case "Remove":
            Handler.Remove();
            break;
        case "List":
            Handler.List();
            break;
        default:
            Handler.List();
            break;
    }
    await DisplayActions();
}

await DisplayActions();
