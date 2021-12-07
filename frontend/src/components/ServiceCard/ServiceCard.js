import React from 'react'
import { Link } from "react-router-dom";

// service info
import ServiceBodyContainer from "components/ServiceCard/ServiceBodyContainer";


import {
    Button,
    Card,
    CardHeader,
    Table,
    Container,
    Row,
    Col,
  } from "reactstrap";

const renderSeeAll = (show) => {
    if (show === true)
    {
        return  <Button>
                    <Link color="blue" to="/admin/tables">See all </Link>
                </Button>
    }
}

const showAllServices = (show) => {
    return show ? <div className="penguin-table-body">
    <Table responsive>
        <ServiceBodyContainer/>
  </Table>
  </div> : <div>
                <Table responsive>
                    <ServiceBodyContainer/>
              </Table>
              </div>
} 


const ServiceCard = (props) => {
    return (
        <Row className="mt-5">
          <Col className="mb-5 mb-xl-0" xl="12">
            <Card className="shadow penguin-card">
              <CardHeader className="border-0">
                <Row className="align-items-center">
                <div className="col">
                    <h3 className="mb-0">Service Info</h3>
                </div>
                <div className="col text-right">
                {renderSeeAll(props.dashboard)}
                </div>
                </Row>
              </CardHeader>
              <div>
              <Table className="align-items-center table-flush penguin-table-head" responsive>
                <thead className="thead-light">
                  <tr>
                    <th scope="col">Service Name</th>
                    <th scope="col">Entrance Time</th>
                    <th scope="col">Exit Time</th>
                    <th scope="col">Last Updated</th>
                  </tr>
                </thead>
                </Table>
              </div>
              {showAllServices(props.dashboard)}
            </Card>
          </Col>
        </Row>
    )
}

export default ServiceCard
