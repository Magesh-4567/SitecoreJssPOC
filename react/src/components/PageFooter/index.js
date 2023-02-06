import React from 'react';
import { Text } from '@sitecore-jss/sitecore-jss-react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import gql from 'graphql-tag';
import GraphQLData from '../../lib/GraphQLData';

const FOOTER_QUERY = gql`
  query FooterComponent($footerSettingId: String) {
    footerNavQuery: item(language: "en", path: $footerSettingId) {
      ... on PageFooter {
        backgroundColor {
          value
        }
        homePageItem {
          jsonValue
        }
        footerTitle {
          value
        }
        navigationItems {
          targetItems {
            ... on MenuItem {
              linkText {
                value
              }
              linkUrl {
                value
              }
            }
          }
        }
      }
    }
  }
`;
class PageFooter extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    console.log(this.props);
    return (
      <div>
        <hr />
        {this.props.footerQ.footerNavQuery && (
          <Navbar bg={this.props.footerQ.footerNavQuery.backgroundColor.value} expand="lg">
            <Container>
              <Navbar.Brand
                href={this.props.footerQ.footerNavQuery.homePageItem.jsonValue.value.href}
              >
                {this.props.footerQ.footerNavQuery.footerTitle.value}
              </Navbar.Brand>
              <Navbar.Collapse id="basic-navbar-nav">
                {this.props.footerQ.footerNavQuery.navigationItems &&
                  this.props.footerQ.footerNavQuery.navigationItems.targetItems.map(
                    (navitem, index) => (
                      <Nav.Link key={index} href={navitem.linkUrl.value}>
                        {navitem.linkText.value}
                      </Nav.Link>
                    )
                  )}
              </Navbar.Collapse>
            </Container>
          </Navbar>
        )}
      </div>
    );
  }
}
export default GraphQLData(FOOTER_QUERY, {
  name: 'footerQ',
  options: {
    variables: {
      footerSettingId: '{73C8CAF5-E358-464F-8495-CE3A1447763E}',
    },
  },
})(PageFooter);
