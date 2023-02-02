import React from 'react';
import Nav from 'react-bootstrap/Nav';

class SideMenu extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <div>
        {this.props.fields.data.datasource.navigationItems &&
          this.props.fields.data.datasource.navigationItems.targetItems.map((navitem, index) => (
            <Nav key={index} defaultActiveyKey="/home" className="flex-column">
              <Nav.Link href={navitem.url.url}>{navitem.title.value}</Nav.Link>
            </Nav>
          ))}
      </div>
    );
  }
}

export default SideMenu;
