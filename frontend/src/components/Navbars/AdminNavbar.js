import { Link } from "react-router-dom";
// reactstrap components
import {
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
  DropdownToggle,
  Form,
  FormGroup,
  InputGroupAddon,
  InputGroupText,
  Input,
  InputGroup,
  NavItem,
  NavLink,
  Navbar,
  Nav,
  Container,
  Media,
} from "reactstrap";

import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const AdminNavbar = (props) => {

    const [ name, setName ] = useState('')
    const { token } = useToken();

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/Auth', {
                method: 'GET',
                headers: {
                    // set token
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                }
            })
            const body = await result.json()
            setName(body.data.name + ' ' + body.data.surName)

        }

        fetchData()
    }, [])

    // sign out funciton
    const signOut = () => {
        localStorage.removeItem('token');
        props.setToken(null);
    }

  return (
    <>
      <Navbar className="navbar-top navbar-dark" expand="md" id="navbar-main">
        <Container fluid>
          <Link
            className="h4 mb-0 text-white text-uppercase d-none d-lg-inline-block"
            to="/"
          >
            {props.brandText}
          </Link>
          <Nav className="align-items-center d-none d-md-flex" navbar>
          
              
            <UncontrolledDropdown nav>
              <DropdownToggle to="/admin/user-profile" tag={Link} className="pr-0" nav>
                <Media className="align-items-center">
                  <span className="avatar avatar-sm rounded-circle">
                    <img
                      alt="..."
                      src={
                        require("../../assets/img/theme/profile.jpg")
                          .default
                      }
                    />
                  </span>
                  <Media className="ml-2 d-none d-lg-block">
                    <span className="mb-0 text-sm font-weight-bold">
                      {name}
                    </span>
                  </Media>
                </Media>
              </DropdownToggle>
            </UncontrolledDropdown>
            <NavItem>
                <NavLink className="nav-link-icon" to="/auth/login" tag={Link}>
                  <i className="ni ni-key-25" />
                  <span onClick={signOut} className="nav-link-inner--text">Çıkış Yap</span>
                </NavLink>
              </NavItem>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
};

export default AdminNavbar;
