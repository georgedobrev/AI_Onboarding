import React from "react";
import { List, ListItem, ListItemIcon } from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import MessageIcon from "@mui/icons-material/Message";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";

const Navbar: React.FC = () => {
  return (
      <div className="navbar">
      <List>
        <ListItem button>
          <ListItemIcon>
            <HomeIcon />
          </ListItemIcon>
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <MessageIcon />
          </ListItemIcon>
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <AccountCircleIcon />
          </ListItemIcon>
        </ListItem>
      </List>
    </div>
  );
};

export default Navbar;
