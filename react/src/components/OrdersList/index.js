import React from 'react';
import Table from 'react-bootstrap/Table';

class OrdersList extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <div>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Order Id</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Detail Page</th>
            </tr>
          </thead>
          <tbody>
            {this.props.fields.data.datasource.children.results &&
              this.props.fields.data.datasource.children.results.map((orderItem, index) => {
                return (
                  <tr key={index}>
                    <td>{orderItem.orderId.value}</td>
                    <td>{orderItem.firstName.value}</td>
                    <td>{orderItem.lastName.value}</td>
                    <td>
                      <a href={`/order-detail?orderId=${orderItem.orderId.value}`}>Result</a>
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </Table>
      </div>
    );
  }
}

export default OrdersList;
