# Получаем путь к текущему скрипту
$currentScriptPath = Split-Path -Path $PSCommandPath -Parent

# Папка `passwords` относительно текущего места
$passwordDirectory = Join-Path -Path $currentScriptPath -ChildPath "passwords"

# Создаём папку, если она не существует
if (-not (Test-Path $passwordDirectory)) {
    New-Item -Path $passwordDirectory -ItemType Directory
}

# Определяем данные для сервисов
$socialMedia = @(
    @{ Name = "Facebook"; URL = "https://www.facebook.com/"; Login = "user_fb"; Password = "fbpassword123" },
    @{ Name = "YouTube"; URL = "https://www.youtube.com/"; Login = "user_yt"; Password = "ytpassword123" },
    @{ Name = "Instagram"; URL = "https://www.instagram.com/"; Login = "user_ig"; Password = "igpassword123" },
    @{ Name = "Twitter"; URL = "https://www.twitter.com/"; Login = "user_tw"; Password = "twpassword123" },
    @{ Name = "Snapchat"; URL = "https://www.snapchat.com/"; Login = "user_sc"; Password = "scpassword123" },
    @{ Name = "LinkedIn"; URL = "https://www.linkedin.com/"; Login = "user_ln"; Password = "lnpassword123" },
    @{ Name = "TikTok"; URL = "https://www.tiktok.com/"; Login = "user_tt"; Password = "tikitoki123" },
    @{ Name = "Pinterest"; URL = "https://www.pinterest.com/"; Login = "user_pt"; Password = "ptpassword123" },
    @{ Name = "Reddit"; URL = "https://www.reddit.com/"; Login = "user_rd"; Password = "rdpassword123" },
    @{ Name = "WhatsApp"; URL = "https://www.whatsapp.com/"; Login = "user_wp"; Password = "wppassword123" }
)

# Создаем файлы для каждого сервиса
foreach ($service in $socialMedia) {
    $filePath = Join-Path -Path $passwordDirectory -ChildPath "$($service.Name).txt"
    $content = "Nom du service: $($service.Name)"
    $content += "`nLien du service: $($service.URL)"
    $content += "`nLogin du compte: $($service.Login)"
    $content += "`nMot de passe du compte: $($service.Password)"
    
    Set-Content -Path $filePath -Value $content
}

Write-Output "10 файлов успешно созданы в $passwordDirectory."
