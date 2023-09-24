
document.getElementById("createMealForm").addEventListener("submit", function (e) {
    e.preventDefault(); // Prevent the default form submission

    var form = e.target;

    var form1 = document.getElementById("createMealForm");
    var Access_Token = form1.getAttribute("data-access-token"); // Retrieve the JWT token from ViewBag

    var headers = new Headers();
    headers.append("Authorization", "Bearer " + Access_Token);

    var requestOptions = {
        method: form.method,
        headers: headers,
        body: new FormData(form)
    };

    fetch(form.action, requestOptions) // Perform the form submission
        .then(response =>
        {
            if (response.status == 401 || response.status == 403)
            {
                window.location.href = 'https://localhost:44349/Account/LogIn';
            }

            else
            {
                return response.text();
            }
        }
           )
        .then(result => {
            // Handle response if needed
        })
        .catch(error => {
            // Handle error if needed
        });
});