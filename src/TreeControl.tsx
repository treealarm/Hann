﻿import * as React from 'react';


import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Checkbox from '@mui/material/Checkbox';
import IconButton from '@mui/material/IconButton';
import { Box, Toolbar } from '@mui/material';



export function TreeControl() {
  
  interface MyObject {
    id: string;
    name: string;
  }

  var markers:MyObject[] = [];

  for (var i = 1; i < 200; i++)
  {
markers.push(
  {
    id:i.toString(),
    name: "item " + i.toString()
  });
  }

    return (
      <Box sx={{
            width: '100%',            
            height: '100%',            
            display: 'flex',
            flexDirection: 'column' 
            }}>
        <Box sx={{ flexGrow: 1, backgroundColor: 'lightgray' }}>
          <Toolbar variant="dense">
            TOOLBAR2
          </Toolbar>
        </Box> 

        <Box sx={{
            width: '100%',            
            height: '100%',            
            overflow: 'auto'
            }}>
        <List dense sx={{
          width: "100%",
          minHeight:'100%',
           backgroundColor: 'lightgreen'
            }}>
         {
          markers?.map((marker, index) =>
            <ListItem
              key={marker.id}
              disablePadding
             >
              <ListItemButton
                >
                <ListItemIcon>
                  <Checkbox
                    size="small"
                    edge="start"
                  
                    tabIndex={-1}
                    disableRipple
                    id={marker.id}
                  
                  />
                </ListItemIcon>
                <ListItemText
                  id={marker.id}
                  primary={marker.name}                  
                />
              </ListItemButton>
          </ListItem>
        )}
          </List>
          </Box>
        </Box>
    );
}