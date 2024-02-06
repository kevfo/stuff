// Assume that jQuery is already available to us...

// Handles the fetching of customers: (i.e., update the other fields in the inline form and also 
// update the table below):
$(document).ready(function() {
    // Handles fetching of customer data and also updating the table of orders:
    $('#change_customer').click(function (event) {
        event.preventDefault();
        let customer = document.querySelector('#customer_name').value;
        $.ajax({
            url: 'ChangeCustomer/GetCustomerInfo',
            type: 'GET',
            datatype: 'json',
            data: { customer_name: customer },
            success: function (data) {
                let customer_id = document.querySelector('#customer_id');
                let table_number = document.querySelector('#customer_table');
                let phone_number = document.querySelector('#customer_phone');
                let date = document.querySelector('#customer_date');

                // Get and update the data here...
                let fetched_data = data[0];
                customer_id.value = fetched_data.customerId; table_number.value = fetched_data.tableNo;
                phone_number.value = `${fetched_data.mobile.slice(0, 4)} ${fetched_data.mobile.slice(4, fetched_data.mobile.length)}`
                date.value = fetched_data.arriveDt;
            },
            error: function (error) {
                alert(`The following error happened: ${error}`);
            }
        })

        // This was blocking the async code:
        // let customer_id = document.querySelector('#customer_id').value
        $.ajax({
            url: 'ChangeCustomer/GetCustomerOrder',
            type: 'GET',
            datatype: 'json',
            data: { customer_name: customer },
            success: function (data) {
                let order_table = document.querySelector('#orders');
                order_table.innerHTML = '';
                for (let i = 0; i < data.length; i++) {
                    let order_item = data[i];
                    order_table.innerHTML += `
                        <tr>
                            <td> ${i + 1} </td>
                            <td> ${order_item.item_name} </td>
                            <td> ${order_item.unit_price} </td>
                            <td>
                                <div class = 'add-plus-item'>
                                    <button class = 'minus-button' onclick = 'decrement(this)'> - </button>
                                    <span> ${order_item.quantity} </span>
                                    <button class = 'plus-button' onclick = 'increment(this)'> + </button>
                                </div>   
                            </td>
                            <td> \$${Number(order_item.unit_price) * Number(order_item.quantity)} </td>
                        </tr>
                    `
                }
            },
            error: function (error) {
                alert(`The following error happened: ${error}`);
            }
        })
    })

    // Handles menu items:
    $('.menu_items').click(function () {
        let menu_item = $(this).text().trim(), customer_id = document.querySelector('#customer_id').value;
        let ordered_items = document.querySelector('#orders').querySelectorAll('tr');
        ordered_items = Array.from(ordered_items).map(row => row.innerText.split('\t')[1]);
        if (ordered_items.includes(menu_item)) {
            alert(`${menu_item} has already been added.`);
            return;
        };

        $.ajax({
            url: 'ChangeOrder/AddItem',
            type: 'POST',
            datatype: 'json',
            data: { menu_item: menu_item, customer_id: customer_id },
            success: function (data) {
                let order_table = document.querySelector('#orders');
                order_table.innerHTML = '';
                for (let i = 0; i < data.length; i++) {
                    let order_item = data[i];
                    order_table.innerHTML += `
                    <tr>
                        <td> ${i + 1} </td>
                        <td> ${order_item.item_name} </td>
                        <td> ${order_item.unit_price} </td>
                        <td>
                            <div class = 'add-plus-item'>
                                <button class = 'minus-button' onclick = 'decrement(this)'> - </button>
                                <span> ${order_item.quantity} </span>
                                <button class = 'plus-button' onclick = 'increment(this)'> + </button>
                            </div>   
                        </td>
                        <td> \$${Number(order_item.unit_price) * Number(order_item.quantity)} </td>
                    </tr>
                    `;
                }
            },
            error: function (error) {
                alert(`The following error happened: ${error}`);
            }
        })
    });

    $('#change_customer').click();
})