 let currentCommentButton = null;

      function showSection(id) {
        document.querySelectorAll(".section").forEach((s) => s.classList.add("d-none"));
        if(document.getElementById(id)) document.getElementById(id).classList.remove("d-none");
        window.scrollTo({ top: 0, behavior: "smooth" });
      }

      function updateTime() {
        const now = new Date();
        document.getElementById("currentTime").textContent = "تاریخ و زمان: " + now.toLocaleString("fa-IR");
      }
      setInterval(updateTime, 1000);
      window.onload = updateTime;

      function confirmDelete(button) {
        const confirmDiv = document.createElement("div");
        confirmDiv.style.position = "fixed";
        confirmDiv.style.top = "0";
        confirmDiv.style.left = "0";
        confirmDiv.style.width = "100%";
        confirmDiv.style.height = "100%";
        confirmDiv.style.backgroundColor = "rgba(0,0,0,0.5)";
        confirmDiv.style.display = "flex";
        confirmDiv.style.justifyContent = "center";
        confirmDiv.style.alignItems = "center";
        confirmDiv.style.zIndex = "2000";

        confirmDiv.innerHTML = `
          <div class="card p-4 text-center" style="min-width: 300px;">
            <p>آیا از حذف مطمئن هستید؟</p>
            <div class="d-flex justify-content-center gap-2 mt-3">
              <button class="btn btn-danger" id="confirmYes">بله</button>
              <button class="btn btn-secondary" id="confirmNo">خیر</button>
            </div>
          </div>
        `;
        document.body.appendChild(confirmDiv);

        document.getElementById("confirmYes").onclick = () => {
          const li = button.closest("li");
          if (li) li.remove();
          confirmDiv.remove();
        };
        document.getElementById("confirmNo").onclick = () => confirmDiv.remove();
      }

      function confirmLogout() {
        const confirmDiv = document.createElement("div");
        confirmDiv.style.position = "fixed";
        confirmDiv.style.top = "0";
        confirmDiv.style.left = "0";
        confirmDiv.style.width = "100%";
        confirmDiv.style.height = "100%";
        confirmDiv.style.backgroundColor = "rgba(0,0,0,0.5)";
        confirmDiv.style.display = "flex";
        confirmDiv.style.justifyContent = "center";
        confirmDiv.style.alignItems = "center";
        confirmDiv.style.zIndex = "2000";

        confirmDiv.innerHTML = `
          <div class="card p-4 text-center" style="min-width: 300px;">
            <p>آیا از خروج از حساب کاربری مطمئن هستید؟</p>
            <div class="d-flex justify-content-center gap-2 mt-3">
              <button class="btn btn-danger" id="logoutYes">بله</button>
              <button class="btn btn-secondary" id="logoutNo">خیر</button>
            </div>
          </div>
        `;
        document.body.appendChild(confirmDiv);

        document.getElementById("logoutYes").onclick = () => {
          confirmDiv.remove();
          alert("شما از حساب کاربری خارج شدید!"); // اینجا می‌توانید logout واقعی اضافه کنید
        };
        document.getElementById("logoutNo").onclick = () => confirmDiv.remove();
      }

      function showComments(articleId) {
        document.querySelectorAll(".section").forEach(s => s.classList.add("d-none"));
        document.getElementById("commentsArticle1").classList.remove("d-none");
        window.scrollTo({ top: 0, behavior: "smooth" });
      }

      function replyComment(button) {
        currentCommentButton = button;
        const replyModal = new bootstrap.Modal(document.getElementById("replyModal"));
        replyModal.show();
      }

      document.getElementById("sendReplyBtn").addEventListener("click", function() {
        const replyText = document.getElementById("replyText").value.trim();
        if (replyText && currentCommentButton) {
          const li = currentCommentButton.closest("li");
          const replyEl = document.createElement("p");
          replyEl.className = "mt-2 mb-0";
          replyEl.style.fontStyle = "italic";
          replyEl.style.color = "#0d6efd";
          replyEl.textContent = "پاسخ: " + replyText;
          li.appendChild(replyEl);
          document.getElementById("replyText").value = "";
          bootstrap.Modal.getInstance(document.getElementById("replyModal")).hide();
        }
      });