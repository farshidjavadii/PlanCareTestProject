# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# --- Stage 0: Base ASP.NET Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# تغییرات پیش‌فرض User و Expose برای اجرای ایمن و دسترسی
ENV ASPNETCORE_URLS=http://+:8080
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# --- Stage 1: Build & Publish ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# کپی کردن فایل های پروژه (csproj) برای بهینه سازی کش Restore
# این مرحله باید شامل تمام پروژه ها باشد
COPY WebApi/WebApi.csproj ./WebApi/
COPY Application/Application.csproj ./Application/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY Domain/Domain.csproj ./Domain/

# اجرای restore روی پروژه اصلی (نیاز به دسترسی به همه csproj های کپی شده دارد)
RUN dotnet restore ./WebApi/WebApi.csproj

# کپی کردن کل سورس کد (فایل های کد C# و Solution)
COPY . .

# تغییر مسیر کار به دایرکتوری WebApi برای Build
WORKDIR "/src/WebApi"

# اجرای Build (بدون restore مجدد). مسیر خروجی -o را حذف می کنیم تا فایل ها در bin/Release پیش فرض قرار بگیرند.
RUN dotnet build -c $BUILD_CONFIGURATION --no-restore

# --- Stage 2: Publish ---
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# اجرای Publish. این دستور فایل های ساخته شده از build/bin را جمع آوری کرده و به /app/publish می برد.
# نکته: در این مرحله نیازی به تغییر WORKDIR نیست زیرا dotnet publish به طور خودکار فایل csproj را در دایرکتوری جاری پیدا می کند.
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-build

# --- Stage 3: Final Runtime Image ---
FROM base AS final
WORKDIR /app
# کپی کردن محتوای پابلیش شده از مرحله قبل
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]