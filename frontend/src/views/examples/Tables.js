// reactstrap components
import {
  Container,
} from "reactstrap";
// core components
import Header from "components/Headers/Header.js";

// service
import ServiceCard from "components/ServiceCard/ServiceCard";

const Tables = () => {
  return (
    <>
      <Header />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <ServiceCard dashboard={false} />
      </Container>
    </>
  );
};

export default Tables;
