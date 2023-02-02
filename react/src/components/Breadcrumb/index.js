import React from 'react';
import Breadcrumb1 from 'react-bootstrap/Breadcrumb';

class Breadcrumb extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    const reversed = this.props.fields.data.contextItem.ancestors.reverse();
    console.log(reversed);
    return (
      <div>
        <Breadcrumb1>
          {reversed &&
            reversed.map((item, index) => (
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
