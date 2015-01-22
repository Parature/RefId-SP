# Sample SAML SP usage with Microsoft Parature
This is a simple example of using a "ReferenceId" token to exchange claims information. This is the slowest form of SSO, but the easiest to understand and use. Comments point to configuration's required for production use. This is provided as-is for demonstration purposes, and should assist in any deep integrations with Parature that require security (Portal or Service Desk).

## Contribute
Please see the separate documentation on [contributing](CONTRIBUTING.md).

## Usage
This is a really basic site that can use a Parature Portal or Service Desk as a RefId Identity Provider (IdP). Steps for testing:

1. [Send a request](http://partners.support.parature.com/) for enabling a Single Sign On Endpoint. Specify:
 * Whether you have an existing Parature Deployment
 * Account and Department Ids (if you have an environment already)
2. We'll respond back with information you'll populate in the Web.Config:
 * _ssoUsername_ - username to pick up the claims information from our server
 * _ssoPassword_ - password for access to pickup the claims information
 * _ssoInstanceId_ - Reference for this specific integration, and required by our servers
 * _ssoIdPUrl_ - A special link to your portal used for SSO

You should be able to run this example application and trace the requests and retrieve the claims data. Status.aspx contains this whole process.