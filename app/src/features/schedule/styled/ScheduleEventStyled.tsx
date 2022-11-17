import styled from 'styled-components';

export const ScheduleEvent = styled.div`
  display: flex;
  flex-direction: column;

  h2 {
    margin: 0;
  }

  > .activities {
    display: flex;
    flex-direction: row;
    gap: 1em;
    flex-wrap: wrap;
  }
`;
