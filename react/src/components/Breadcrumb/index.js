import React from 'react';
import Breadcrumb1 from 'react-bootstrap/Breadcrumb';

class Breadcrumb extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <div>
        <Breadcrumb1>
          {this.props.fields.data.contextItem.ancestors &&
            this.props.fields.data.contextItem.ancestors.map((item, index) => (
              <Breadcrumb1.Item key={index} href={item.url.url}>
                {item.name}
              </Breadcrumb1.Item>
            ))}
          <Breadcrumb1.Item active>{this.props.fields.data.contextItem.name}</Breadcrumb1.Item>
        </Breadcrumb1>
      </div>
    );
  }
}

export default Breadcrumb;
