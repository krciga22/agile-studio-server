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
Backlog Item Types are used to categorize Backlog Items. Common types include Epics, Stories, Defects, Tasks, etc. 

### Child Backlog Item Types
Child Backlog Item Types define which types are available when creating child backlog items.
For example, a Story can have a Task as a child, but a Task cannot have a Story as a child.

### Backlog Item Type Schemas
Backlog Item Type Schemas are used to group backlog item types, so that they can be reused accross different projects.

### Workflows
Workflows are used to define sets of states and transitions, and can be assigned to specific backlog item types.

Example workflows:

| Workflow | Backlog Item Type |
| --- | --- |
| Story & Defect Workflow | Story |
| Story & Defect Workflow | Defect |
| Hotfix Workflow | Hotstory |
| Hotfix Workflow | Hotfix |
| Task Workflow | Task |
| Test Workflow | Test |
| Sub-Task Workflow | Sub-Task |
| Sub-Test Workflow | Sub-Test |

### Workflow States
A workflow can have as many states as needed, and each state must be categorized as either: `Not Started`, `In Progress`, `Complete`.

Example states for a Story & Defect Workflow:

| State | Category |
| --- | --- |
| In Backlog | Not Started |
| In Planning | Not Started |
| In Development | In Progress |
| In Testing | In Progress |
| In Release | Complete |
| Cancelled | Complete |

### Workflow Transitions
Workflow transitions define which states can transition `to` or `from` other states.

Example transitions for a Story & Defect Workflow:

| From State | To State |
| --- | --- |
| In Backlog | In Planning |
| In Planning | In Backlog |
| In Planning | In Development |
| In Development | In Planning |
| In Development | In Testing |
| In Testing | In Development |
| In Testing | In Release |
| In Release | In Testing |
| Any | Cancelled |

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