import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

class PageHeader extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    console.log(this.props);
    return (
      <div>
        {this.props.fields.headerNavResults && (
          <Navbar bg={this.props.fields.headerNavResults.backgroundColor} expand="lg">
            <Container>
              <Navbar.Brand href={this.props.fields.headerNavResults.homePageUrl}>
                {this.props.fields.headerNavResults.headerTitle}
              </Navbar.Brand>
              <Navbar.Toggle aria-controls="basic-navbar-nav" />
              <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                  {this.props.fields.headerNavResults.mainNavigationItems &&
                    this.props.fields.headerNavResults.mainNavigationItems.map(
                      (mainnavitem, index) => (
                        <NavDropdown
                          key={index}
                          title={mainnavitem.linkText}
                          id="basic-nav-dropdown"
                        >
                          <Nav.Link href={mainnavitem.linkUrl}>{mainnavitem.linkText}</Nav.Link>
                          {mainnavitem.subNavigationItems &&
                            mainnavitem.subNavigationItems.map((subnavitem, index) => (
                              <NavDropdown.Item key={index} href={subnavitem.linkUrl}>
                                {subnavitem.linkText}
                              </NavDropdown.Item>
                            ))}
                        </NavDropdown>
                      )
                    )}
                </Nav>
              </Navbar.Collapse>
            </Container>
          </Navbar>
        )}
      </div>
    );
  }
}

export default PageHeader;
