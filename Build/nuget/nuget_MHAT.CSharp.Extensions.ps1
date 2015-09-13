Push-Location $PSScriptRoot
nuget pack ..\..\Src\MHAT.CSharp.Extensions\MHAT.CSharp.Extensions.csproj -build -Prop Configuration=Release
Pop-Location