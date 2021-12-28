import { Link } from "react-router-dom";
import { Button, Container, Row, Col } from "reactstrap";

const UserHeader = () => {
  return (
    <>
      <div
        className="header pb-8 pt-5 pt-lg-8 d-flex align-items-center"
        style={{
          minHeight: "600px",
          backgroundImage:
            "url(" +
            require("../../assets/img/theme/profile-cover.jpg").default +
            ")",
          backgroundSize: "cover",
          backgroundPosition: "center top",
        }}
      >
        {/* Mask */}
        <span className="mask bg-gradient-default opacity-8" />
        {/* Header container */}
        <Container className="d-flex align-items-center" fluid>
          <Row>
            <Col lg="7" md="10">
              <h1 className="display-2 text-white">Hello Habil</h1>
              <p className="text-white mt-0 mb-5">
                This is your profile page. You can see your profile info and you can edit it. You can also see your image and can change it.
              </p>
              <Button
                >
                
              <Link
                 color="white"
                //onClick={(e) => e.preventDefault()}
                to="/admin/edit-profile"
              >
                Edit profile
              </Link>
              </Button>
              <Button
                >
                
              <Link
                 color="white"
                //onClick={(e) => e.preventDefault()}
                to="/admin/add-service"
              >
                Servis Ekle
              </Link>
              </Button>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
};

export default UserHeader;
