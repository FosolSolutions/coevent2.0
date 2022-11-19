import styled from 'styled-components';

export const Day = styled.div`
  display: flex;
  flex-direction: row;
  gap: 1em;

  /* background-color: ${(props) => props.theme.css.filterBackgroundColor}; */
  border-radius: 0.25em;
  padding: 0.5em;
  width: 100%;

  > .date {
    background-color: ${(props) => props.theme.css.light};
    border-radius: 0.25em;
    padding: 0.5em;
  }

  > .events {
    display: flex;
    flex-direction: column;
    width: 100%;
  }
`;
