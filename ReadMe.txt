Для наката миграций 
	dotnet ef migrations add InitialCreate --project "./Infrastructure/Infrastructure.csproj" --startup-project "./Api/Api.csproj"

Накатить миграции
	dotnet ef database update --project "./Infrastructure/Infrastructure.csproj" --startup-project "./Api/Api.csproj"