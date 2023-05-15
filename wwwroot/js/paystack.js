////////////////////
const paymentForm = document.getElementById('paymentForm');
paymentForm.addEventListener("submit", payWithPaystack, false);
function payWithPaystack(e) {
    e.preventDefault();

    let handler = PaystackPop.setup({
        key: 'pk_test_18de9362e665b126982fc285e8f819fd74862be6', // Replace with your public key
       // first_name: document.getElementById("first-name").value,
      //  last_name: document.getElementById("last-name").value,
        email: document.getElementById("email-address").value,
        amount: document.getElementById("amount").value * 100,
        currency: 'NGN',
        ref: document.getElementById("ref").value, // generates a pseudo-unique reference. Please replace with a reference you generated. Or remove the line entirely so our API will generate one for you.
         //label: "Optional string that replaces customer email",
        onClose: function () {
            alert('Window closed.');
        },
        callback: function (response) {
            //this happens after the payment is completed successfully
            const data = response.reference;
            const status = response.status;
            alert('Payment complete! Kindly copy the Reference: ' + data);
            if (status == "success") {
                // Make an AJAX call to your server with the reference to verify the transaction
                $.ajax({
                    type: 'POST',
                    url: '/wallets/UpdateWallet',
                    //contentType: 'application/json', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
                    data: { data: data },
                    success: function () {
                        window.location = "https://localhost:7229/order/receipt"
                        alert('Successfully received Data');
                    },
                    error: function () {
                        alert('Failed to receive the Data');
                    }
                    })
                //////////////////////////////////////////////////////////////////////////
                //This guy sends the referecnce to the backend for another check before proceeding to receipt on success
                //////////////////////////////////////////////////////////////////////////
                //$.ajax({
                //    type: 'POST',
                //    url: '/Wallets/Verify',
                //    //contentType: 'application/json', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
                //    data: { data: data },
                //    success: function () {
                        //window.location = "https://localhost:7117/Wallets/receipt"
                        //alert('Successfully received Data ');
                    //},
                    //error: function () {
                        //alert('Failed to receive the Data');
                    //}
                //})
            }
        },
        onClose: function () {
            alert('Transaction was not completed, window closed.');
        },
    });

    handler.openIframe();
}

