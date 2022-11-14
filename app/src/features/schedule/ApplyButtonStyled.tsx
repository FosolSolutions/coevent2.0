import styled from 'styled-components';

export const ApplyButton = styled.div`
  cursor: pointer;

  svg.on path {
    color: ${(props) => props.theme.css.dangerColor};
  }
  svg.off path {
    color: ${(props) => props.theme.css.primaryColor};
  }
`;
