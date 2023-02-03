import React from 'react';
import Table from 'react-bootstrap/Table';

const OrderDetail = (props) => {
  const orderItem = props.fields.orderResult;
  return (
    <div>
      {orderItem && (
        <div>
          <h3>Order Id: {orderItem.orderId}</h3>
          <p>Created Date: {orderItem.dateCreated}</p>

          {orderItem.customerDetails && (
            <div>
              <h3>Customer Details: </h3>
              <Table striped bordered hover>
                <thead>
                  <tr>
                    <th>MembershipNumber</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>DOB</th>
                    <th>Postal Code</th>
                    <th>Policy Number</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>{orderItem.customerDetails.membershipNumber}</td>
                    <td>{orderItem.customerDetails.firstName}</td>
                    <td>{orderItem.customerDetails.lastName}</td>
                    <td>{orderItem.customerDetails.dob}</td>
                    <td>{orderItem.customerDetails.postalCode}</td>
                    <td>{orderItem.customerDetails.policyNumber}</td>
                  </tr>
                </tbody>
              </Table>
            </div>
          )}

          {orderItem.treatmentDetails && (
            <div>
              <h3>Treament Details: </h3>
              <Table striped bordered hover>
                <thead>
                  <tr>
                    <th>Treatment Id</th>
                    <th>Treatment Name</th>
                    <th>Treatment Description</th>
                  </tr>
                </thead>
                <tbody>
                  {orderItem.treatmentDetails.map((treatmentItem, index) => {
                    return (
                      <tr key={index}>
                        <td>{treatmentItem.treatmentId}</td>
                        <td>{treatmentItem.treatmentName}</td>
                        <td>{treatmentItem.treatmentDescription}</td>
                      </tr>
                    );
                  })}
                </tbody>
              </Table>
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default OrderDetail;
