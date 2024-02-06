// JavaScript source code
increment = (button_element) => {
    let menu_item = button_element.parentNode.parentNode.parentNode.querySelectorAll('td')[1].innerText.trim()
    let customer_id = document.querySelector('#customer_id').value;

    $.ajax({
        url: 'ChangeOrder/IncrementItem',
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
                `
            }
        },
        error: function (error) {
            alert(`The following error happened: ${error}`);
        }
    })
};

decrement = (button_element) => {
    let menu_item = button_element.parentNode.parentNode.parentNode.querySelectorAll('td')[1].innerText.trim()
    let customer_id = document.querySelector('#customer_id').value;

    $.ajax({
        url: 'ChangeOrder/DecrementItem',
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
                `
            }
        },
        error: function (error) {
            alert(`The following error happened: ${error}`);
        }
    })
};