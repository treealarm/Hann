import * as React from 'react';


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

  for (var i = 0; i < 100; i++)
  {
markers.push(
  {
    id:i.toString(),
    name:i.toString()
  });
  }

    return (
      <Box sx={{ height: '100%' }}>
      <Box sx={{
            width: '100%',            
            height: '100%'
            }}>
        <Box sx={{ flexGrow: 1, backgroundColor: 'lightgray' }}>
          <Toolbar variant="dense">
            <IconButton edge="start">
              
            </IconButton>
            <Box sx={{ flexGrow: 1 }} />
            <IconButton edge="end">
              
            </IconButton>
          </Toolbar>
        </Box> 

        <Box sx={{
            width: '100%',            
            height: 'calc(100% - 60px)',
            overflow: 'auto'
            }}>
        <List dense sx={{
          width: "100%",
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
        </Box>
    );
}