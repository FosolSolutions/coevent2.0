import styled from 'styled-components';

export const ScheduleEvent = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0.25em;
  border-radius: 0.5em;
  border: solid 1px ${(props) => props.theme.css.light};
  background-color: ${(props) => props.theme.css.light};

  h2,
  h3 {
    margin: 0;
  }

  > .header {
    display: flex;
    flex-direction: row;
    gap: 1em;

    svg {
      color: ${(props) => props.theme.css.iconLightColor};
    }

    h3 {
      color: ${(props) => props.theme.css.secondaryVariantColor};
    }
  }

  > .activities {
    display: flex;
    flex-direction: row;
    gap: 1em;
    flex-wrap: wrap;
  }

  &.series-1 {
    background-color: ${(props) => props.theme.css.lightAccentColor};
  }

  &.series-2 {
    background-color: ${(props) => props.theme.css.primary};
  }

  &.series-3 {
    background-color: ${(props) => props.theme.css.success};
  }

  &.series-4 {
    background-color: ${(props) => props.theme.css.info};
  }

  &.series-5 {
    background-color: ${(props) => props.theme.css.danger};
  }
`;
