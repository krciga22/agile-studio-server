# Agile Studio Server
Agile Studio Server is the backend for Agile Studio. It provides essential agile project management services and exposes public apis for Agile Studio Client or other clients to consume. 
It is currently a single tenant solution.

## Services Provided

### Projects
Projects allow development teams to manage separate codebases or areas of development. Each project has it's own team, backlog, settings, etc.

### Project Teams
Projects are managed by a team of users, each of which has a specific level of access (e.g. Owner, Developer, QA, Viewer) to the project.

### Backlog Items
Backlog items represent tasks/work to be done within a project.

### Backlog Item Types
Backlog Item Types are used to categorize Backlog Items. Common types include Stories, Defects, General Tasks, Epics, etc. 
Backlog Item Types are created within a Backlog Item Type Schema which can be assigned to specific projects.

### Backlog Item Type Schemas
Backlog Item Type Schemas are used to group backlog item types, so that they can be reused accross different projects.

### Releases
Releases represent versions or deployments of software. Backlog items can be assigned to specific releases.
and when all items assigned are completed, the release can then be deployed.

### Sprints
Sprints represent periods of work (e.g. Jan 1 - Jan 14). Backlog items can be assigned to specific sprints--allowing 
team memers to focus only on backlog items in the current sprint. Once all backlog items assigned to the sprint have 
been completed, the sprint can then be closed and the next sprint opened.

### Custom Fields
Custom fields are used to collect custom data when managing backlog items (e.g. resolution, test steps, external id, etc.). 
Custom fields are managed at the system level and can be assigned to specific backlog item types.

### Custom Field Controls
Custom Field Controls represent the physical inputs/widgets (text, text area, number, etc.) used to collect and display data for a custom field. 
Every Custom Field must be assigned a Custom Field Control.

### Custom Field Values
Custom Field Values represent values stored by a custom field. Some custom fields store a single value while 
others, such as lists, need to store multiple values. Each value stored is stored on it's own and is associated 
with a custom field and backlog item.

### Users
New users are able to create and start managing their own projects. They can also be invited to 
join existing projects.

### Authentication
Agilestudio uses JWT authentication. After authenticating, clients need to store the JWT token received and 
provide it when requesting other services.