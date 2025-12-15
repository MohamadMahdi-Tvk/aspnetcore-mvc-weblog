const themeToggle = document.getElementById("themeToggle");
const body = document.body;

  // تغییر حالت
themeToggle.addEventListener("click", () => {
    body.classList.toggle("dark-mode");

themeToggle.textContent = body.classList.contains("dark-mode")
      ? "☀️ حالت روز"
      : "🌙 حالت شب";

    // ذخیره در LocalStorage
    localStorage.setItem("theme", body.classList.contains("dark-mode") ? "dark" : "light");
  });

  // بازیابی حالت ذخیره شده
window.onload = () => {
const saved = localStorage.getItem("theme");
if (saved === "dark") {
      body.classList.add("dark-mode");
      themeToggle.textContent = "☀️ حالت روز";
}};