const paymentForm = document.getElementById('paymentForm');
paymentForm.addEventListener("submit", payWithPaystack, false);

function payWithPaystack(e) {
    e.preventDefault();

    // Make an AJAX request to retrieve the payment key
    fetch('/wallets/GetPaymentKey')
        .then(response => response.json())
        .then(data => {
            // Use the retrieved payment key
            let handler = PaystackPop.setup({
                key: data,
                email: document.getElementById("email-address").value,
                amount: document.getElementById("amount").value * 100,
                currency: 'NGN',
                ref: document.getElementById("ref").value,
                onClose: function () {
                    alert('Window closed.');
                },
                callback: function (response) {
                    // Handle payment completion
                    const data = response.reference;
                    const status = response.status;
                    alert('Payment complete! Kindly copy the Reference: ' + data);
                    if (status == "success") {
                        // Make an AJAX call to your server with the reference to verify the transaction
                        $.ajax({
                            type: 'POST',
                            url: '/wallets/UpdatePayment',
                            data: { data: data },
                            success: function () {
                                window.location = "https://localhost:1111/wallets/receipt"
                                alert('Successfully received Data');
                            },
                            error: function () {
                                alert('Failed to receive the Data');
                            }
                        });
                    }
                },
                onClose: function () {
                    alert('Transaction was not completed, window closed.');
                }
            });

            handler.openIframe();
        })
        .catch(error => {
            console.error('Failed to retrieve payment key:', error);
        });
}
